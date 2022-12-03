using System;
using System.CodeDom;
using System.Collections.Generic;

namespace ObjectGenerator
{
    public class ObjectTypeMapper
    {
        private readonly Type[] types;
        private readonly string[] propertyNames;
        private readonly string className;

        public ObjectTypeMapper(Type[] types, string[] propertyNames, string className)
        {
            this.types = types;
            this.propertyNames = propertyNames;
            this.className = className;
        }

        public void GenerateType(IDictionary<string, Type> publicProperties)
        {
            CodeCompileUnit compileUnit = new CodeCompileUnit();
            var exampleNamespace = new CodeNamespace("ExampleNamespace");
            exampleNamespace.Imports.Add(new CodeNamespaceImport("System"));
            compileUnit.Namespaces.Add(exampleNamespace);

            var codeMemberProperty = new CodeMemberProperty();

            foreach (var property in publicProperties)
            {

                codeMemberProperty.Name = property.Key;
                codeMemberProperty.Type = new CodeTypeReference(property.Value);
                codeMemberProperty.Attributes = MemberAttributes.Public;
                codeMemberProperty.GetStatements.Add(new CodeMethodReturnStatement());
                codeMemberProperty.Name += "{get; set;}//";

                
            }
        }
    }
}
