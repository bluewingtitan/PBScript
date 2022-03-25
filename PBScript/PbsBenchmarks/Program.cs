using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

var summaries = BenchmarkRunner.Run(typeof(Program).Assembly);