module Picums.Virtuals.Interfaces

    open System
    open System.Threading.Tasks

    type IContentRequest =
        abstract member LoadTextFileAsync: string -> Task<string>