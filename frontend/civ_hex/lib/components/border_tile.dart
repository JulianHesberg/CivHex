import 'package:flutter/material.dart';
import 'package:hexagon/hexagon.dart';

import 'hex_tile.dart';
class BorderTile extends StatefulWidget{
  HexTile parentHex;

  BorderTile({super.key, required this.parentHex});

  @override
  _BorderTileState createState() => _BorderTileState();
}

class _BorderTileState extends State<BorderTile>{



  @override
  Widget build(BuildContext context) {
    return HexagonWidget.pointy(
      width: 100,
      color: ,
    )
  }
  
}