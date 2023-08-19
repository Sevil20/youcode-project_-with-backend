using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis;
using System.Reflection;

namespace YouCode.Services
{
    public static class CompilerService
    {
        public static string RunUserCode(string code)
        {
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(code);

            string assemblyName = Path.GetRandomFileName();
            MetadataReference[] references = new MetadataReference[]
            {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location)
            };

            CSharpCompilation compilation = CSharpCompilation.Create(
                assemblyName,
                syntaxTrees: new[] { syntaxTree },
                references: references,
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            using (MemoryStream ms = new MemoryStream())
            {
                EmitResult result = compilation.Emit(ms);

                if (!result.Success)
                {
                    string errorMessages = string.Join(Environment.NewLine, result.Diagnostics.Select(d => d.GetMessage()));
                    return "Derleme Hataları: " + errorMessages;
                }
                else
                {
                    ms.Seek(0, SeekOrigin.Begin);
                    Assembly assembly = Assembly.Load(ms.ToArray());
                    Type type = assembly.GetType("DynamicCodeRunner.DynamicCodeRunner");

                    object instance = Activator.CreateInstance(type);
                    MethodInfo method = type.GetMethod("Run");
                    string output = (string)method.Invoke(instance, null);

                    return output;
                }
            }
        }
    }
}
