﻿namespace FlouPoint.CLI.TestGeneration.Strategies.Generation.ExpressionGenerator.AST
{
    public class TypeArguments
    {
        public List<string> ArgumentTypes { get; }

        public TypeArguments(List<string> argumentTypes)
        {
            ArgumentTypes = argumentTypes;
        }
    }
}