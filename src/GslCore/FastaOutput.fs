module fastaOutput

open System.IO
open System
open commonTypes
open constants
open Amyris.Bio.utils
open utils
open Amyris.Dna
open Genbank
open pragmaTypes

/// Emit Fasta format
///  outDir : string
let dumpFasta
    (path : string)
    (assemblies : DnaAssembly list) =

    printfn "Writing fasta output to path=%s" path
    use outF = new StreamWriter(path)

    for assembly in assemblies do
        let assemblyId = match assembly.id with | None -> "" | Some id -> sprintf "A%d|" (id+1)
        outF.WriteLine $">Assembly|{assemblyId}{assembly.name}"
        let assembledDna =
            assembly.dnaParts
            |> List.map (fun p -> p.dna.str) |> String.concat ""
            |> (fun x -> x.ToCharArray() |> Amyris.Bio.utils.format60)
        outF.WriteLine(assembledDna)
        for part in assembly.dnaParts do
            if part.dna.Length > 0 then
                let partId = match part.id with | None -> "" | Some id -> sprintf "P%d|" (id+1)
                outF.WriteLine $">Part|{assemblyId}{partId}{part.description}"
                let partDna = part.dna.arr |> Amyris.Bio.utils.format60
                outF.WriteLine(partDna)
