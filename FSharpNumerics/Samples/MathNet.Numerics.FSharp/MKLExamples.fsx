#r "../../../packages/MathNet.Numerics.2.6.2/lib/net40/MathNet.Numerics.dll"
#r "../../../packages/MathNet.Numerics.FSharp.2.6.0/lib/net40/MathNet.Numerics.FSharp.dll"

open System.Numerics
open MathNet.Numerics
open MathNet.Numerics.Distributions
open MathNet.Numerics.Random
open MathNet.Numerics.LinearAlgebra.Double
open MathNet.Numerics.LinearAlgebra.Generic
open MathNet.Numerics.Algorithms.LinearAlgebra.Mkl
open MathNet.Numerics.Algorithms.LinearAlgebra
open System.IO

// NB: To run this using the the 64 bit MKL, please ensure that
// you're using the 64 bit FSI.  To ensure that this is the case, 
// Tools -> Options -> F# Tools -> 64bit F# Interactive -> True

let setWorkingDirectoryToPointToMKLAssemblies() = // see http://christoph.ruegg.name/blog/loading-native-dlls-in-fsharp-interactive.html
    let workingDir = Path.Combine( __SOURCE_DIRECTORY__, @"..\..\")
    System.Environment.CurrentDirectory <- workingDir

let getRandomMatrices() = 
    let dim = 1000
    let d = Normal.WithMeanVariance(0.0, 1.0)
    let a = DenseMatrix.randomCreate dim dim d
    let b = DenseMatrix.randomCreate dim dim d
    a,b

let testLAProvider provider a b = 
    MathNet.Numerics.Control.LinearAlgebraProvider <- provider
    stopWatch.Restart()
    let c = a*b
    stopWatch.Stop()
    let totalTime = stopWatch.ElapsedMilliseconds 
    printfn "-------------------------------------------" 
    printfn "That took %i milliseconds with %s provider." totalTime (provider.GetType().Name)
    printfn "-------------------------------------------" 

setWorkingDirectoryToPointToMKLAssemblies()
let a,b = getRandomMatrices()

testLAProvider (MklLinearAlgebraProvider()    ) a b
testLAProvider (ManagedLinearAlgebraProvider()) a b