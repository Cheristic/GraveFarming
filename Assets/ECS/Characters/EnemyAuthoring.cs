using Unity.Entities;
using UnityEngine;

public class EnemyAuthoring : MonoBehaviour
{
    public float health = 10f ;
    public float movespeed = 3f;
    public float attackrange = 0.5f;
    // This class, Baker, is embedded in the EnemyAuthoring class directly (though
    // it doesn't have to be, this is just nice and clean). It manages the baking
    // process that converts this GameObject to an Entity
    public class EnemyBaker : Baker<EnemyAuthoring>
    {
        // The one method of this class. This is where the baking work is done
        public override void Bake(EnemyAuthoring authoring)
        {

            var entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new EnemyTag { });

            AddComponent(entity, new Health { Value = authoring.health });

            AddComponent(entity, new MoveSpeed { Value = authoring.movespeed });

            AddComponent(entity, new AttackRange { Value = authoring.attackrange });
        }
    }
}