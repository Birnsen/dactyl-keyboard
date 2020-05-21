module Dactyl.KeyHoles
open Dactyl.Placement
open Dactyl.SingleJoint
open Dactyl.Variables
open OpenSCAD.Fs.Lib.Combinator
open OpenSCAD.Fs.Lib
open OpenSCAD.Fs.Lib.Operator
open Dactyl.SinglePlate


let part1 =
    let block y =
        jointBlock
        |> rotate 90.0<deg> [0.0; 0.0; 1.0]
        |> translate [(mount_width / 2.0) - 1.5; y; 0.0]

    let sideBlock x =
        jointBlock
        |> translate [x; 0.0; 0.0]

    let jointBlocks =
        //Top
        [ key_place 0.0 0 (jointBlockTl (block 1.3))
        ; key_place 1.0 0 (jointBlockTl (block 1.3))
        //Bottom
        ; key_place 0.0 4 (jointBlockTl (block -1.1))
        ; key_place 1.0 4 (jointBlockTl (block -1.1))
        //Sides
        ; key_place 0.0 1 (jointBlockTl (sideBlock 0.2))
        ; key_place 0.0 2 (jointBlockTl (sideBlock 0.2))
        ; key_place 0.0 3 (jointBlockTl (sideBlock 0.2))
        ; key_place 1.0 1 (jointBlockTr (sideBlock -0.2))
        ; key_place 1.0 2 (jointBlockTr (sideBlock -0.2))
        ; key_place 1.0 3 (jointBlockTr (sideBlock -0.2))
        ] 
        |> List.collect id

    let fst = keyHoleColumn 0.0 cornerrow
    let snd = keyHoleColumn 1.0 cornerrow
    let conCols = connectionColumn 0.0 cornerrow
    let conFstRow = connectionRow 0.0 (cornerrow)
    let conSndRow = connectionRow 1.0 (cornerrow)
    let conDiag = connectionDiagonal 0.0 (cornerrow)

    [fst; snd
    ; conCols
    ; conFstRow; conSndRow
    ; conDiag
    ; jointBlocks] |> List.collect id |> union

let part2 = 
    let blockLeftRows =
        let slope =
            centeredCube [3.0; 8.0; 7.0]
            |> rotate 45.0<deg> [0.0; 1.0; 0.0]
            |> translate [-4.0; 0.0; 4.5]

        let block = 
            centeredCube [5.0; 7.0; 7.0]
            |> translate [-3.9; 0.0; 0.5]
            
        [block; slope]
        |> List.collect id
        |> difference

    let blockRightRows deg =
        let slope =
            centeredCube [4.0; 8.0; 15.0]
            |> rotate (deg * 1.0<deg>) [0.0; 1.0; 0.0]
            |> translate [-4.0; 0.0; 5.0]

        let block = 
            centeredCube [5.0; 7.0; 7.0]
            |> translate [-3.9; 0.0; 0.5]
            
        [block; slope]
        |> List.collect id
        |> difference

    let jointBolts =
        let jbl =
            blockLeftRows
            |> jointBoltLeftWithBlock
            |> jointBlockTr
            |> translate [0.1; 0.0; 0.0]

        let jbr deg =
            deg
            |> blockRightRows 
            |> jointBoltRightWithBlock
            |> jointBlockTl
            |> translate [-0.1; 0.0; 0.0]

        let block y =
            jointBlock
            |> rotate 90.0<deg> [0.0; 0.0; 1.0]
            |> translate [(mount_width / 2.0) - 1.5; y; 0.0]
            
        //Top
        [ key_place 2.0 0 (jointBlockTl (block 1.3))
        ; key_place 3.0 0 (jointBlockTl (block 1.3))
        //bottom
        ; key_place 2.0 5 (jointBlockTl (block -1.0))
        ; key_place 3.0 5 (jointBlockTl (block -0.9))
        //Sides
        ; key_place 1.0 1 jbl
        ; key_place 1.0 2 jbl
        ; key_place 1.0 3 jbl
        ; key_place 4.0 1 (jbr 25.0)
        ; key_place 4.0 2 (jbr 45.0)
        ; key_place 4.0 3 (jbr 65.0)
        ] 
        |> List.collect id

    let fst = keyHoleColumn 2.0 lastrow
    let snd = keyHoleColumn 3.0 lastrow
    let conFstCols = connectionColumn 1.0 cornerrow
    let conSndCols = connectionColumn 2.0 lastrow
    let conThdCols = connectionColumn 3.0 cornerrow
    let conFstRow = connectionRow 2.0 (lastrow)
    let conSndRow = connectionRow 3.0 (lastrow)
    let conFstDiag = connectionDiagonal 1.0 (cornerrow)
    let conSndDiag = connectionDiagonal 2.0 (lastrow)
    let conThdDiag = connectionDiagonal 3.0 (cornerrow)

    [fst; snd
    ; conFstCols; conSndCols; conThdCols
    ; conFstRow; conSndRow 
    ; conFstDiag; conSndDiag; conThdDiag
    ; jointBolts] |> List.collect id |> union

let part3 = 
    let block y =
        jointBlock
        |> rotate 90.0<deg> [0.0; 0.0; 1.0]
        |> translate [(mount_width / 2.0) - 1.5; y; 0.0]

    let sideBlock x =
        jointBlock
        |> translate [x; 0.0; 0.0]

    let jointBlocks =
        //Top
        [ key_place 4.0 0 (jointBlockTl (block 1.3))
        ; key_place 5.0 0 (jointBlockTl (block 1.3))
        //Bottom
        ; key_place 4.0 4 (jointBlockTl (block -0.9))
        ; key_place 5.0 4 (jointBlockTl (block -0.9))
        //Sides
        ; key_place 4.0 1 (jointBlockTl (sideBlock 0.2))
        ; key_place 4.0 2 (jointBlockTl (sideBlock 0.2))
        ; key_place 4.0 3 (jointBlockTl (sideBlock 0.2))
        ; key_place 5.0 1 (jointBlockTr (sideBlock -0.2))
        ; key_place 5.0 2 (jointBlockTr (sideBlock -0.2))
        ; key_place 5.0 3 (jointBlockTr (sideBlock -0.2))
        ] 
        |> List.collect id

    let fst = keyHoleColumn 4.0 cornerrow
    let snd = keyHoleColumn 5.0 cornerrow
    let conCols = connectionColumn 4.0 cornerrow
    let conFstRow = connectionRow 4.0 (cornerrow)
    let conSndRow = connectionRow 5.0 (cornerrow)
    let conDiag = connectionDiagonal 4.0 (cornerrow)


    [fst; snd
    ; conCols
    ; conFstRow; conSndRow
    ; conDiag
    ; jointBlocks ] |> List.collect id |> union







