#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Practices.CompositeUI.Configuration
{
    /// <summary>
    /// Base class for config sections that accept arbitrary name/value pairs.
    /// Unknown keys in a configuration section are gathered into <see cref="Parameters"/>.
    /// </summary>
    public abstract class ParametersElement
    {
        /// <summary>
        /// Arbitrary parameters captured from configuration (unknown keys).
        /// </summary>
        public IDictionary<string, string> Parameters { get; } =
            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Call after binding the derived type to capture any extra (unknown) keys.
        /// </summary>
        public void LoadParameters(IConfigurationSection section, bool flattenNested = true, string? separator = ":")
        {
            // Known property names on the derived type (so we don't capture them as "unknown").
            var known =
                GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Select(p => p.Name)
                .Append(nameof(Parameters))
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            if (flattenNested)
            {
                foreach (var (key, value) in EnumerateLeafKeys(section, separator ?? ":"))
                {
                    // Skip keys that exactly match known top-level property names
                    var topLevelKey = key.Split(new[] { separator ?? ":" }, StringSplitOptions.None)[0];
                    if (!known.Contains(topLevelKey) && value is not null)
                    {
                        Parameters[key] = value;
                    }
                }
            }
            else
            {
                foreach (var child in section.GetChildren())
                {
                    // Only capture simple values at this level
                    if (!known.Contains(child.Key) && child.Value is string v)
                    {
                        Parameters[child.Key] = v;
                    }
                }
            }
        }

        private static IEnumerable<(string Key, string? Value)> EnumerateLeafKeys(
            IConfigurationSection section, string separator)
        {
            // Depth-first walk of the section tree; emit only leaves (key -> value).
            var stack = new Stack<IConfigurationSection>();
            stack.Push(section);

            while (stack.Count > 0)
            {
                var current = stack.Pop();
                var children = current.GetChildren().ToList();

                if (children.Count == 0)
                {
                    // Leaf: current.Path is like "Root:Child:Leaf" (or "Root" if top-level)
                    yield return (current.Path.Substring(section.Path.Length).TrimStart(':'),
                                  current.Value);
                }
                else
                {
                    // Push children; Path composes with ':'
                    foreach (var c in children)
                        stack.Push(c);
                }
            }
        }
    }

    public static class ParametersElementBindingExtensions
    {
        /// <summary>
        /// Binds a configuration section to T and captures any unknown keys into <see cref="ParametersElement.Parameters"/>.
        /// </summary>
        public static T BindWithParameters<T>(this IConfigurationSection section,
                                              bool flattenNested = true,
                                              string? separator = ":")
            where T : ParametersElement, new()
        {
            var instance = new T();
            section.Bind(instance);                 // Bind known properties first
            instance.LoadParameters(section, flattenNested, separator);
            return instance;
        }
    }
}
