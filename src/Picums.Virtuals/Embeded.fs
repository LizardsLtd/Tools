module Picums.Virtuals.Embeded

    open System
    open System.Threading.Tasks
    open System.Reflection
    open Picums.Virtuals.Interfaces
    open System.IO

    type EmbeddedContentLoader(assemblyName: string) =
        class
            member this.assembly = Assembly.Load(new AssemblyName(assemblyName));

            interface  IContentRequest with 
                member this.LoadTextFileAsync(resourceHandle: string) : Task<string> =
                    let fullResourceHandle = this.CreateFullyQualifiedResourceName(resourceHandle);
                    let resourceStream = this.assembly.GetManifestResourceStream(fullResourceHandle);
                    let content = 
                        use reader = new StreamReader(resourceStream)
                        reader.ReadToEndAsync()
                    content

            member this.GetAssemblyName(): string = this.assembly.GetName().Name

            member this.CreateFullyQualifiedResourceName(resourceHandle: string): string =
                String.Join(".", this.GetAssemblyName(), resourceHandle);
        end 
