using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FallDave.Trifles.Xml.XPath
{
    /// <summary>
    /// A pair of dictionaries, one for functions and one for variables, mapped from <see
    /// cref="XName"/> objects (a tuple of a namespace URI and a local name, with no assumed prefix).
    /// Lookup can be within this object alone, or can fall back to a parent table if one was specified.
    /// </summary>
    public class XPathXNamesTable
    {
        private XPathXNamesTable parent;

        private readonly ParentedMap<XName, TriflesXPathExtensionFunction> functions;
        private readonly ParentedMap<XName, TriflesXPathExtensionVariable> variables;
        
        /// <summary>
        /// Initialized a names table.
        /// </summary>
        /// <param name="parent"></param>
        public XPathXNamesTable(XPathXNamesTable parent = null)
        {
            ParentedMap<XName, TriflesXPathExtensionFunction> parentFunctions = null;
            ParentedMap<XName, TriflesXPathExtensionVariable> parentVariables = null;

            if(parent != null)
            {
                parentFunctions = parent.functions;
                parentVariables = parent.variables;
            }
            
            this.parent = parent;
            this.functions = ParentedMap.Create(parentFunctions);
            this.variables = ParentedMap.Create(parentVariables);
        }

        /// <summary>
        /// Adds a function to this table under the given name.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="function"></param>
        public void AddFunction(XName name, TriflesXPathExtensionFunction function)
        {
            functions.Add(name, function);
        }

        /// <summary>
        /// Adds a function to this table under its own name (<see cref="TriflesXPathExtensionFunction.FunctionName"/>).
        /// </summary>
        /// <param name="function"></param>
        public void AddFunction(TriflesXPathExtensionFunction function)
        {
            functions.Add(function.FunctionName, function);
        }

        /// <summary>
        /// Retrieves, from this table alone, the function having the given name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Opt<TriflesXPathExtensionFunction> GetOwnFunctionOpt(XName name)
        {
            return functions.GetOwnOpt(name);
        }

        /// <summary>
        /// Retrieves, from this table or its ancestors, the nearest-scoped function having the given name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Opt<TriflesXPathExtensionFunction> GetFunctionOpt(XName name)
        {
            return functions.GetOpt(name);
        }

        /// <summary>
        /// Adds a variable to this table under the given name.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="variable"></param>
        public void AddVariable(XName name, TriflesXPathExtensionVariable variable)
        {
            variables.Add(name, variable);
        }

        /// <summary>
        /// Adds a variable to this table under its own name (<see cref="TriflesXPathExtensionVariable.VariableName"/>).
        /// </summary>
        /// <param name="variable"></param>
        public void AddVariable(TriflesXPathExtensionVariable variable)
        {
            variables.Add(variable.VariableName, variable);
        }

        /// Retrieves, from this table alone, the variable having the given name.
        public Opt<TriflesXPathExtensionVariable> GetOwnVariableOpt(XName name)
        {
            return variables.GetOwnOpt(name);
        }

        /// <summary>
        /// Retrieves, from this table or its ancestors, the nearest-scoped variable having the given name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Opt<TriflesXPathExtensionVariable> GetVariableOpt(XName name)
        {
            return variables.GetOpt(name);
        }
    }
}
