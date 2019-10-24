using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using RDotNet;

namespace fshapex.app.Domain
{
    public class R
    {
        private readonly string _rHome;
        private readonly string _rPath;
        private REngine Engine { get; set; }

        public R()
        {
            //var programFiles = Environment.GetFolderPath(Environment.Is64BitProcess ? Environment.SpecialFolder.ProgramFiles : Environment.SpecialFolder.ProgramFilesX86);
            //_rHome = Path.Combine(programFiles, "FShapeX", "R-3.4.4");
            //_rPath = Path.Combine(_rHome, Environment.Is64BitProcess ? @"bin\x64" : @"bin\i386");
            _rHome = "C:/FShapeX/R-3.4.4";
            _rPath = _rHome + "/bin/" + (Environment.Is64BitProcess ? "x64" : "i386");

            REngine.SetEnvironmentVariables(_rPath, _rHome);

            Engine = REngine.GetInstance();

            //Engine.Evaluate($".libPaths('{_rHome.Replace("\\", "/")}/library')");
            Engine.Evaluate($".libPaths('{_rHome}/library')");
        }

        public SymbolicExpression Evaluate(int k)
        {
            return Engine.Evaluate($"source('C:/FShapeX/App/Scripts/k{k}.r')");
        }
    }
}
