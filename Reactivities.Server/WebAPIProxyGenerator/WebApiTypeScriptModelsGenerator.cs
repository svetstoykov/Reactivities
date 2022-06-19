﻿using System.Reflection;
using API.Models.Common;
using Models.Common;
using TypeScriptModelsGenerator;

namespace WebAPIGenerator
{
    public static class WebApiTypeScriptModelsGenerator
    {
        private const string AutoGeneratedFolderTemplate = "{0}\\client\\src\\autogenerated";

        private static string ClientAutoGenerated => GetAutoGeneratedFolder();
        private static Type BaseApiType => typeof(BaseApiModel);

        public static void CleanUpTypeScriptModels()
        {
            if (Directory.Exists(ClientAutoGenerated))
            {
                Directory.Delete(ClientAutoGenerated, true);
            }
        }

        public static void GenerateTypeScriptModels()
        {
            var apiModels = 
                GetAssemblyModels(BaseApiType.Assembly, (t) => BaseApiType.IsAssignableFrom(t));

            TypeScriptModelsGeneration
                .Setup(apiModels, ClientAutoGenerated)
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
