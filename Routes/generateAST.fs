module GenerateAST

open Microsoft.AspNetCore.Http
open System.Web
open Giraffe
open Absyn
open Parse  
open Fun

let parseHandler : HttpHandler =
    fun (next: HttpFunc) (ctx: HttpContext) ->
        task {
            use reader = new System.IO.StreamReader(ctx.Request.Body)
            let! input = reader.ReadToEndAsync()
            let decodedInput = HttpUtility.UrlDecode(input)

            let prefix = "treeInput="
            let cleanedInput = decodedInput.Substring(prefix.Length)
            printfn "Input: %s" cleanedInput

            
            
            let parsedExpr = Parse.fromString cleanedInput  // parse to Absyn.expr
            printfn "Parsed: %A" parsedExpr
            let result = run parsedExpr                     // evaluate it
            return! json result next ctx                    // return JSON
        }


