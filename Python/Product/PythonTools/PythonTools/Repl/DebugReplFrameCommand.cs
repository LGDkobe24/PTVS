// Python Tools for Visual Studio
// Copyright(c) Microsoft Corporation
// All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the License); you may not use
// this file except in compliance with the License. You may obtain a copy of the
// License at http://www.apache.org/licenses/LICENSE-2.0
//
// THIS CODE IS PROVIDED ON AN  *AS IS* BASIS, WITHOUT WARRANTIES OR CONDITIONS
// OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING WITHOUT LIMITATION ANY
// IMPLIED WARRANTIES OR CONDITIONS OF TITLE, FITNESS FOR A PARTICULAR PURPOSE,
// MERCHANTABLITY OR NON-INFRINGEMENT.
//
// See the Apache Version 2.0 License for specific language governing
// permissions and limitations under the License.

using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.PythonTools.Debugger.DebugEngine;
using System;
using Microsoft.PythonTools.Debugger;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using System.Collections.Generic;
using Microsoft.VisualStudio.Utilities;
using Microsoft.PythonTools.InteractiveWindow;
using Microsoft.PythonTools.InteractiveWindow.Commands;

namespace Microsoft.PythonTools.Repl {
    [Export(typeof(IInteractiveWindowCommand))]
    [InteractiveWindowRole("Debug")]
    [ContentType(PythonCoreConstants.ContentType)]
    class DebugReplFrameCommand : IInteractiveWindowCommand {
        public Task<ExecutionResult> Execute(IInteractiveWindow window, string arguments) {
            var eval = window.GetPythonDebugReplEvaluator();
            if (eval != null) {
                if (string.IsNullOrEmpty(arguments)) {
                    eval.DisplayActiveFrame();
                } else {
                    int id;
                    if (int.TryParse(arguments, out id)) {
                        eval.ChangeActiveFrame(id);
                    } else {
                        window.WriteError(String.Format("Invalid arguments '{0}'. Expected frame id.", arguments));
                    }
                }
            }
            return ExecutionResult.Succeeded;
        }

        public string Description {
            get { return "Switches the current frame to the specified frame id."; }
        }

        public string Command {
            get { return "frame"; }
        }

        public IEnumerable<ClassificationSpan> ClassifyArguments(ITextSnapshot snapshot, Span argumentsSpan, Span spanToClassify) {
            yield break;
        }

        public string CommandLine {
            get {
                return "";
            }
        }

        public IEnumerable<string> DetailedDescription {
            get {
                yield return Description;
            }
        }

        public IEnumerable<KeyValuePair<string, string>> ParametersDescription {
            get {
                yield break;
            }
        }

        public IEnumerable<string> Names {
            get {
                yield return Command;
            }
        }
    }
}
