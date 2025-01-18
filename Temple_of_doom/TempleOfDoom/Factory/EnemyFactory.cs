using TempleOfDoom.data.Models.Map;
using TempleOfDoom.data.DTO;
using TempleOfDoom.data.Models.Enemy;

namespace TempleOfDoom.Factory;

public static class EnemyFactory
{
    public static Enemy CreateEnemy(EnemyDto enemyDTO)
    {
        var position = new Position(enemyDTO.X, enemyDTO.Y);
        return new Enemy(enemyDTO.Type, position, enemyDTO.MinX, enemyDTO.MaxX, enemyDTO.MinY, enemyDTO.MaxY);
    }
}
