using BenchmarkDotNet.Running;

var summaries = BenchmarkRunner.Run(typeof(Program).Assembly);