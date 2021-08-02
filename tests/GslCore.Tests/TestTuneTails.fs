namespace GslCore.Tests
open System
open NUnit.Framework
open gslcProcess
open commonTypes
open AssemblyTestSupport
open Amyris.Dna
open DesignParams
open Amyris.Bio.primercore

[<TestFixture>]
type TestTuneTails() =

    // enable for detailed (very detailed) output from procAssembly. Useful for debugging test cases
    let verbose = true

    do
        // initialize pragmas
        pragmaTypes.finalizePragmas []

    [<Test>]
    member __.PathologicalCase1() =
        let dp : DesignParams = { targetTm = 52.0<C>
                                  seamlessOverlapTm = 68.0<C>
                                  pp = Amyris.Bio.primercore.defaultParams
                                  overlapParams = {defaultParams with maxLength = 80}
                                  overlapMinLen = 24 }

        let fwd : Primer = { tail = Dna("TTGAATATTCCCTCAAAA")
                             body = Dna("ATGTCTCAAAAAAAGAAGGC")
                             annotation = [] }

        let rev = { tail = Dna("TTTTGAGGGAATATTCAA")
                    body = Dna("TATAGTTTTTTCTCCTTGAC")
                    annotation = [] }

        let _result = PrimerCreation.tuneTails
                        verbose
                        dp
                        None // fwdTailLenFixed
                        10 // fwdTailLenMin
                        2147483627 // fwdTailLenMax
                        None // firmMiddle
                        None // (revTailLenFixed:int option)
                        9 // revTailLenMin
                        2147483647 // revTailLenMax
                        fwd
                        rev
                        (Dna("TTGAATATTCCCTCAAAA")) // (middleDNA : Dna) =
        ()
    [<Test>]
    member __.PathologicalCase2() =
        let dp : DesignParams = { targetTm = 52.0<C>
                                  seamlessOverlapTm = 68.0<C>
                                  pp = Amyris.Bio.primercore.defaultParams
                                  overlapParams = {defaultParams with maxLength = 80}
                                  overlapMinLen = 24 }

        let rev : Primer = { tail = Dna("TTGAATATTCCCTCAAAA")
                             body = Dna("ATGTCTCAAAAAAAGAAGGC")
                             annotation = [] }

        let fwd = { tail = Dna("TTTTGAGGGAATATTCAA")
                    body = Dna("TATAGTTTTTTCTCCTTGAC")
                    annotation = [] }

        let _result = PrimerCreation.tuneTails
                        verbose
                        dp
                        None // fwdTailLenFixed
                        10 // fwdTailLenMin
                        2147483627 // fwdTailLenMax
                        None // firmMiddle
                        None // (revTailLenFixed:int option)
                        9 // revTailLenMin
                        2147483647 // revTailLenMax
                        fwd
                        rev
                        (Dna("TTGAATATTCCCTCAAAA")) // (middleDNA : Dna) =
        ()
