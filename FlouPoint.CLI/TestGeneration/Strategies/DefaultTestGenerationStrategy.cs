﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlouPoint.CLI.TestGeneration.Interfaces;

namespace FlouPoint.CLI.TestGeneration.Strategies
{
    public class DefaultTestGenerationStrategy : ITestGenerationStrategy
    {
        public string GenerateTestCode(string className, string propertyName)
        {
            // Provide a generic implementation or throw an exception
            throw new NotSupportedException($"Test generation for type '{propertyName}' is not supported.");
        }
    }
}