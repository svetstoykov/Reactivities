using System.Diagnostics;
using WebAPIProxyGenerator;

Debug.WriteLine("Generating TypeScript Models...");

WebApiTypeScriptModelsGenerator.CleanUpTypeScriptModels();
WebApiTypeScriptModelsGenerator.GenerateTypeScriptModels();