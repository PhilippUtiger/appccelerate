//-------------------------------------------------------------------------------
// <copyright file="ArgumentTransitionActionHolder.cs" company="Appccelerate">
//   Copyright (c) 2008-2012
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
// </copyright>
//-------------------------------------------------------------------------------

namespace Appccelerate.StateMachine.Machine.ActionHolders
{
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Holds a transition action with exactly one argument.
    /// </summary>
    /// <typeparam name="T">The type of the argument.</typeparam>
    public class ArgumentTransitionActionHolder<T> : ITransitionActionHolder
    {
        private readonly Action<T> action;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentTransitionActionHolder{T}"/> class.
        /// </summary>
        /// <param name="action">The action.</param>
        public ArgumentTransitionActionHolder(Action<T> action)
        {
            this.action = action;
        }

        /// <summary>
        /// Executes the transition action.
        /// </summary>
        /// <param name="argument">The state machine event argument.</param>
        public void Execute(object argument)
        {
            if (argument != null && !(argument is T))
            {
                throw new ArgumentException(ActionHoldersExceptionMessages.CannotCastArgumentToActionArgument(argument, this.Describe()));
            }

            this.action((T)argument);
        }

        /// <summary>
        /// Describes the action.
        /// </summary>
        /// <returns>Description of the action.</returns>
        public string Describe()
        {
            return this.action.Method.GetCustomAttributes(typeof(CompilerGeneratedAttribute), false).Any() ? "anonymous" : this.action.Method.Name;
        }
    }
}