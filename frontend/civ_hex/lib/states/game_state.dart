import '../components/hex_tile.dart';
import '../models/player.dart';

class GameState {
  List<Player> players;
  List<HexTile> hexTiles;
  num turnNumber;

  GameState(
      {required this.players,
      required this.hexTiles,
      required this.turnNumber});
}
