/// Assembly transforming plugin that injects linker assignments into the flow
module LinkerPlugin

open System.IO
open commonTypes
open commandConfig
open PluginTypes
open Amyris.ErrorHandling

let linkeredArg =
   {name = "seamless";
    param = ["(true or false)"];
    alias = [];
    desc = "Perform seamless assembly."}


type LinkeredAssembler = {
    run: bool
    /// Optionally attach a function to this plugin behavior to permit its operation to be
    /// configured by command line arguments injected by other plugins.  This is necessary because
    /// linkered assembly can alter a lot of expectations of downstream processing steps.
    processExtraArgs: ParsedCmdLineArg -> LinkeredAssembler -> LinkeredAssembler}
    with
    interface IAssemblyTransform with
        member x.ProvidedArgs() = [SeamlessPlugin.seamlessArg] // this has to match the seamless defintion exactly to keep compiler happy
        member x.Configure(arg) =
            if arg.spec = linkeredArg then
                let run =
                    match arg.values with
                    | ["true"] -> false
                    | ["false"] -> true // note opposite of seamless
                    | [x] -> failwithf "Invalid argument for linkered: '%s'. Options are 'true' or 'false'." x
                    | _ -> failwithf "Linkered plugin received the wrong number of command line arguments."
                {x with run = run}
            else x
            |> x.processExtraArgs arg
            :> IAssemblyTransform
        member x.ConfigureFromOptions(opts) =
            if opts.noPrimers then
                {x with run = false}
            else x
            :> IAssemblyTransform
        member x.TransformAssembly context assembly =
            if x.run then
                // get linker file
                // csv formatted in library folder
                let path = Path.Combine(context.opts.libDir, "linkers.txt")

                let linkers = ryse.loadRyseLinkers path
                ok (ryse.mapRyseLinkers context.opts Map.empty linkers assembly)
            else
                ok assembly

/// Produce an instance of the seamless assembly plugin with the provided extra argument processor.
let createLinkerPlugin defaultRun extraArgProcessor =
   {name = "linkered_assembly";
    description = Some "Perform linkered assembly by assigning linkers beteween slices (judiciously)."
    behaviors =
      [{name = None;
        description = None;
        behavior = AssemblyTransform({run = defaultRun; processExtraArgs = extraArgProcessor})}]
    providesPragmas = [];
    providesCapas = []}

let linkerPlugin = createLinkerPlugin true (fun _ x -> x)
