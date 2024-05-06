import 'package:flutter/cupertino.dart';

import 'border_tile.dart';
import 'hex_tile.dart';

class GameBoard extends StatefulWidget{
  List<HexTile> hexTiles;
  List<BorderTile> borderTiles;

  GameBoard({super.key, required this.hexTiles, required this.borderTiles});


  @override
  _GameBoardState createState() => _GameBoardState();

}

class _GameBoardState extends State<GameBoard>{
  @override
  Widget build(BuildContext context) {
    // TODO: implement build
    throw UnimplementedError();
  }

}
