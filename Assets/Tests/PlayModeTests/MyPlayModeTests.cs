using System;
using System.Collections;
using Assets.Scripts;


using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.TestTools;
using Object = UnityEngine.Object;

public class MyPlayModeTests
{
    // A Test behaves as an ordinary method
    //[Test]
    //public void MyPlayModeTestsSimplePasses()
    //{
    //    // Use the Assert class to test conditions
    //}

    //// A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    //// `yield return null;` to skip a frame.
    //[UnityTest]
    //public IEnumerator MyPlayModeTestsWithEnumeratorPasses()
    //{
    //    // Use the Assert class to test conditions.
    //    // Use yield to skip a frame.
    //    yield return null;
    //}

    [UnityTest]
    public IEnumerator PlayerAttack_DamagesEnemyCorrectly()
    {
        // Create a player GameObject with the PlayerCombat component
        var playerGO = new GameObject("Player");
        var playerCombat = playerGO.AddComponent<PlayerCombat>();
        
        playerCombat.AttackDamage = 30;
        playerCombat.AttackRange = 5f;
        playerCombat.EnemyLayers = LayerMask.GetMask("Enemy"); // we'll set this layer

        // Optional: set an animator so it doesn't null ref
        playerGO.AddComponent<Animator>();

        // Set player's layer to something *other* than "Enemy" so it doesn't hit itself
        playerGO.layer = LayerMask.NameToLayer("Default");

        // Create an enemy GameObject with CharacterStats
        var enemyGO = new GameObject("Enemy");
        var characterStats = enemyGO.AddComponent<CharacterStats>();
        
        characterStats.MaxHealth = 100;
        characterStats.Defense = 10;
        enemyGO.layer = LayerMask.NameToLayer("Enemy");

        // Position the enemy within attack range
        playerGO.transform.position = Vector3.zero;
        enemyGO.transform.position = new Vector3(1, 0, 0); // 1 unit away, within range

        // Wait a frame so Awake runs
        yield return null;

        // Make sure enemy is fully initialized
        Assert.AreEqual(100, characterStats.CurrentHealth);

        // Manually invoke Attack
        var stats = playerGO.GetComponent<PlayerCombat>()
                            .GetType()
                            .GetMethod("Attack", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                            .Invoke(playerCombat, null);

        yield return null; // wait a frame to allow for any damage effects

        // Expected damage = AttackDamage (30) - Defense (10) = 20
        Assert.AreEqual(80, characterStats.CurrentHealth);

        // Cleanup
        Object.Destroy(playerGO);
        Object.Destroy(enemyGO);
    }
}

public class UnityTestAttribute : Attribute
{
}
