﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NeetBOT.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("NeetBOT.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to [&quot;Random name generator&quot;,&quot;Higher/lower, heads/tails&quot;,&quot;Temperature converter&quot;,&quot;Calculate age in seconds&quot;,&quot;Encryption/decryption algorithm&quot;,&quot;Fizzbuzz&quot;,&quot;Rock, paper, scissors&quot;,&quot;Three Project Euler problems&quot;,&quot;Hangman&quot;,&quot;Travesty generator&quot;,&quot;Password/passphrase generator&quot;,&quot;Internet time&quot;,&quot;Haiku generator&quot;,&quot;Magic eight ball&quot;,&quot;Collatz conjecture&quot;,&quot;Reverse a string&quot;,&quot;Eulerian path&quot;,&quot;Simple file explorer&quot;,&quot;Count words in a string&quot;,&quot;Minesweeper&quot;,&quot;Connect Four&quot;,&quot;BMI calculator&quot;,&quot;4chan thread/image downloader&quot;,&quot;Sudoku g [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string challenges {
            get {
                return ResourceManager.GetString("challenges", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        internal static byte[] InsultData {
            get {
                object obj = ResourceManager.GetObject("InsultData", resourceCulture);
                return ((byte[])(obj));
            }
        }
    }
}
