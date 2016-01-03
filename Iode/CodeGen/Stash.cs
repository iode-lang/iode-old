﻿using Iode.AST;
using Iode.Library;
using Iode.Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace Iode.CodeGen
{
    /// <summary>
    /// Contains variable lists and such
    /// </summary>
    public class Stash
    {
        public static Dictionary<string, Node> variables = new Dictionary<string, Node>()
        {
            { "version_iode", new StringNode("1.0.0.0") }
        };

        public static Dictionary<string, LibraryMethod> builtIns = new Dictionary<string, LibraryMethod>()
        {
            { "puts", new Puts() },
            { "puts_i", new PutsNumber() }
        };

        public static Dictionary<string, UserMethod> userMethods = new Dictionary<string, UserMethod>();

        /// <summary>
        /// Checks if the specified variable has been declared yet
        /// </summary>
        /// <param name="name">Variable name</param>
        /// <returns>Boolean</returns>
        public static bool variableExists(string name)
        {
            return variables.Keys.Contains(name);
        }

        /// <summary>
        /// Deletes the specified variable
        /// </summary>
        /// <param name="name">Variable name</param>
        public static void deleteVariable(string name)
        {
            variables.Remove(name);
        }

        /// <summary>
        /// Creates a new variable
        /// </summary>
        /// <param name="name">Variable name</param>
        /// <param name="value">Variable value</param>
        public static void pushVariable(string name, Node value)
        {
            // not going to use variables.add(.., ..);
            variables[name] = value;
        }

        /// <summary>
        /// Gets the value of the specified variable
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Node getVariable(string name)
        {
            return variables[name];
        }

        /// <summary>
        /// Checks if there is a built-in method with the specified name
        /// </summary>
        /// <param name="name">Method name</param>
        /// <returns>Boolean</returns>
        public static bool libMethodExists(string name)
        {
            return builtIns.Keys.Contains(name);
        }

        /// <summary>
        /// Gets the method of a specific name
        /// </summary>
        /// <param name="name">Method name</param>
        /// <returns>LibraryMethod</returns>
        public static LibraryMethod getLibMethod(string name)
        {
            return builtIns[name];
        }

        /// <summary>
        /// Checks if there is a user-made method with the specified name
        /// </summary>
        /// <param name="name">Method name</param>
        /// <returns>Boolean</returns>
        public static bool methodExists(string name)
        {
            return userMethods.Keys.Contains(name);
        }

        /// <summary>
        /// Gets the method of a specific name
        /// </summary>
        /// <param name="name">Method name</param>
        /// <returns>UserMethod</returns>
        public static UserMethod getMethod(string name)
        {
            return userMethods[name];
        }

        /// <summary>
        /// Generates a dynamic method from a specific user method
        /// </summary>
        /// <param name="name">Name of the method</param>
        /// <param name="mod">Method's parent module</param>
        /// <returns>DynamicMethod</returns>
        public static DynamicMethod generateMethod(string name, Module mod)
        {
            if (methodExists(name))
            {
                return new DynamicMethod(name, getMethod(name).returnType, getMethod(name).types, mod);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Creates a new method
        /// </summary>
        /// <param name="um">Method</param>
        public static void pushMethod(UserMethod um)
        {
            userMethods[um.name] = um;
        }
    }
}
