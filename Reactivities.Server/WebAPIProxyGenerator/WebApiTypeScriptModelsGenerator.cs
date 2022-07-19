﻿using System.Reflection;
using API.Common;
using Models.Common;
using TypeScriptModelsGenerator;

namespace WebAPIGenerator
{
    public static class WebApiTypeScriptModelsGenerator
    {
        private const string AutoGeneratedFolderTemplate = "{0}\\client\\src\\autogenerated";

        private static string ClientAutoGeneratedFolder => GetAutoGeneratedFolder();
        private static Type ClientGenerationModelType => typeof(BaseApiModel);

        public static void CleanUpTypeScriptModels()
        {
            if (Directory.Exists(ClientAutoGeneratedFolder))
            {
                Directory.Delete(ClientAutoGeneratedFolder, true);
            }
        }

        public static void GenerateTypeScriptModels()
        {
            var apiModels = 
                GetAssemblyModels(ClientGenerationModelType.Assembly, (t) => ClientGenerationModelType.IsAssignableFrom(t));

            TypeScriptModelsGeneration
                .Setup(apiModels, ClientAutoGeneratedFolder)
                .Execute();
        }

        private static string GetAutoGeneratedFolder()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            while (currentDirectory.Split('\\').Last() != GlobalConstants.Reactivities)
            {
                var parentDirectory = Directory.GetParent(currentDirectory);
                if (parentDirectory == null)
                {
                    throw new Exception("Invalid path.");
                }

                currentDirectory = parentDirectory.FullName;
            }

            return string.Format(AutoGeneratedFolderTemplate, currentDirectory);
        }

        private static IEnumerable<Type> GetAssemblyModels(Assembly assembly, Func<Type, bool> predicate = null)
        {
            var types = assembly.GetExportedTypes();

            return predicate == null 
                ? types 
                : types.Where(predicate);
        }
    }
}
