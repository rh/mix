using System;

namespace Mix.Core.Attributes
{
    /// <summary>
    /// An attribute to mark properties as an XML argument.
    /// Properties marked with this attribute will be automatically validated.
    /// </summary>
    /// <example>
    /// In the following example, AddFragmentAction.Fragment is marked as an XML
    /// argument:
    /// <code>
    /// public class AddFragmentAction : Action
    /// {
    ///     [Argument, XmlArgument]
    ///     public string Fragment
    ///     {
    ///         get { return fragment; }
    ///         set { fragment = value; }
    ///     }
    ///     // etc.
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class XmlArgumentAttribute : Attribute
    {
    }
}