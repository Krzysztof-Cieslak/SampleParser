// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System


let code = """

open System

type X = {x: int}

let x =
    1

let from whom =
    sprintf "from %s" whom

let print () =
   printfn "Hello world"

let add a b =
    a + b

"""


[<EntryPoint>]
let main argv =
    let lines = code.Split "\n"
    let x =
        lines
        |> List.ofArray
        |> List.fold (fun acc currentLine ->
            match acc with
            | (true,currentStateOfTheFunction)::tail when (currentLine.StartsWith " ")  ->
                (true, currentLine::currentStateOfTheFunction)::tail
            | _ ->
                if (currentLine.StartsWith "let " && currentLine.EndsWith "=\r") then
                    let fragments = currentLine.Split " "
                    if fragments.Length > 3 then
                        (true, [currentLine]) :: acc
                    else
                        acc
                else
                    acc


        ) []
        |> List.map (fun (_, functionBody) -> functionBody |> List.rev |> String.concat "\n")

    printfn "%A" x



    0 // return an integer exit code