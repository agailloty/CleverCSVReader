using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace ObjectGenerator
{
    public class ObjectTypeMapper
    {
        private readonly IDictionary<string, Type> publicProperties;
        private readonly string className;

        public ObjectTypeMapper(IDictionary<string, Type> publicProperties, string className)
        {
            this.publicProperties = publicProperties;
            this.className = className;
        }

        public void GenerateTypeCsharp(System.IO.TextWriter writer, string lang = "Csharp")
        {
            CodeCompileUnit compileUnit = new CodeCompileUnit();
            var exampleNamespace = new CodeNamespace("ExampleNamespace");
            exampleNamespace.Imports.Add(new CodeNamespaceImport("System"));
            compileUnit.Namespaces.Add(exampleNamespace);

            var targetType = new CodeTypeDeclaration
            {
                Name = className,
                IsClass = true,
                TypeAttributes = System.Reflection.TypeAttributes.Public,
            };

 
            var codeMemberProperty = new CodeMemberProperty();

            foreach (var property in publicProperties)
            {
                codeMemberProperty.Name = property.Key;
                codeMemberProperty.Type = new CodeTypeReference(property.Value);
                codeMemberProperty.Attributes = MemberAttributes.Public | MemberAttributes.Final;
                //codeMemberProperty.GetStatements.Add(new CodeMethodReturnStatement());
                codeMemberProperty.Name += " { get; set; } //";
                targetType.Members.Add(codeMemberProperty);
                codeMemberProperty = new CodeMemberProperty();
            }

            exampleNamespace.Types.Add(targetType);
            var provider = CodeDomProvider.CreateProvider(lang);
            var options = new CodeGeneratorOptions{ BracingStyle = "C"};

            provider.GenerateCodeFromCompileUnit(compileUnit, writer, options);
        }
    }
}
