@echo off
echo Running benchmark test..
echo Press any key to stop
PerformanceTest localhost 6 123qwe! > BenchmarkTest.txt
PerformanceAnalyzer Logs\Feed.t2.log Logs\Feed.t3.log >> BenchmarkTest.txt
PerformanceAnalyzer Logs\Trade.t0.log Logs\Trade.t1.log >> BenchmarkTest.txt
PerformanceAnalyzer Logs\Trade.t2.log Logs\Trade.t3.log >> BenchmarkTest.txt
