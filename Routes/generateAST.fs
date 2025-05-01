module GenerateAST

open Fun

let parseHandler =
    fun (next: HttpFunc) (ctx: HttpContext) ->
        task {
            let! input = ctx.BindModelAsync<string>() // Get user input from POST body
            let parsedAst = run input // Process MicroML AST
            return! json parsedAst next ctx // Return AST as JSON
        }
    

