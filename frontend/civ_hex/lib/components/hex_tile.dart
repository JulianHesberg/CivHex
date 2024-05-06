import 'package:civ_hex/enums/tile_status.dart';
import 'package:civ_hex/enums/tile_type.dart';
import 'package:civ_hex/models/player.dart';
import 'package:flutter/material.dart';
import 'package:hexagon/hexagon.dart';

class HexTile extends StatefulWidget {
  final TileType tileType;
  final TileStatus tileStatus;
  final int tileNumber;
  final Player owner;

  HexTile({super.key,
    required this.tileType,
    required this.tileStatus,
    required this.tileNumber,
    required this.owner,
  });

  @override
  _HexTileState createState() => _HexTileState();
}

class _HexTileState extends State<HexTile> {
  // This is where you can define methods and properties that can change over time

  void _handleTap() {
    print('HexTile tapped: ${widget.tileNumber}');
    // Add your code here
  }

  Color getTileColorByTileType(){
    switch(widget.tileType){
      case TileType.wood:
        return Colors.brown;
      case TileType.sheep:
        return Colors.green;
      case TileType.stone:
        return Colors.grey;
      case TileType.grain:
        return Colors.yellow;
    }
  }

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      onTap: _handleTap,
      child: HexagonWidget.pointy(
        width: 90,
        color: getTileColorByTileType(),
        elevation: 8,
        child: Text(widget.tileNumber.toString()),
      ),
    );
  }
}