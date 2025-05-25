using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Assets.Scripts;

using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class NewTestScript
{
    // A Test behaves as an ordinary method
    [Test]
    public void NewTestScriptSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator NewTestScriptWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }

    [Test]
    public void PlayerAttack_DamagesEnemyCorrectly()
    {
        // Arrange
        var player = new GameObject("Player");
        player.transform.position = Vector3.zero;

        var playerCombat = player.AddComponent<PlayerCombat>();
        var playerStats  = player.AddComponent<CharacterStats>();

        var enemy = new GameObject("Enemy");
        enemy.transform.position = new Vector3(1, 0, 0); // Within 2f AttackRange
        enemy.layer              = LayerMask.NameToLayer("Enemy");    // Match EnemyLayers

        var enemyStats = enemy.AddComponent<CharacterStats>();
        enemyStats.CurrentHealth = 100;

        // Configure PlayerCombat
        playerCombat.AttackRange  = 2f;
        playerCombat.AttackDamage = 20;
        playerCombat.EnemyLayers  = LayerMask.GetMask("Enemy");

        // Act
        var method = typeof(PlayerCombat).GetMethod("Attack", BindingFlags.NonPublic | BindingFlags.Instance);
        method.Invoke(playerCombat, null);

        // Assert
        Assert.AreEqual(80, enemyStats.CurrentHealth);
    }

    //[UnityTest]
    //public IEnumerator PlayerAttack_DamagesEnemyCorrectly()
    //{
    //    var playerObject = new GameObject("Player");
    //    var playerCombat = playerObject.AddComponent<PlayerCombat>();

    //    playerCombat.AttackDamage = 30;
    //    playerCombat.AttackRange = 5f;
    //    playerCombat.EnemyLayers = LayerMask.GetMask("Enemy");

    //    playerObject.AddComponent<Animator>();

    //    playerObject.layer = LayerMask.NameToLayer("Default");

    //    var enemyObject = new GameObject("Enemy");
    //    var characterStats = enemyObject.AddComponent<CharacterStats>();

    //    characterStats.MaxHealth = 100;
    //    characterStats.Defense = 10;
    //    enemyObject.layer = LayerMask.NameToLayer("Enemy");

    //    // Position the enemy within attack range
    //    playerObject.transform.position = Vector3.zero;
    //    enemyObject.transform.position = new Vector3(1, 0, 0); // 1 unit away, within range

    //    // Wait a frame so Awake runs
    //    yield return null;

    //    Assert.AreEqual(100, characterStats.CurrentHealth);

    //    // Manually invoke Attack
    //    var stats = playerObject.GetComponent<PlayerCombat>()
    //                        .GetType()
    //                        .GetMethod("Attack"
    //                                 , System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
    //                        .Invoke(playerCombat
    //                              , null);
        
    //    var enemyStats = enemyObject.GetComponent<CharacterStats>();

    //    yield return null; // wait a frame to allow for any damage effects

    //    // Expected damage = AttackDamage (30) - Defense (10) = 20
    //    Assert.AreEqual(80, characterStats.CurrentHealth);

    //    // Cleanup
    //    Object.Destroy(playerObject);
    //    Object.Destroy(enemyObject);
    //}
}
