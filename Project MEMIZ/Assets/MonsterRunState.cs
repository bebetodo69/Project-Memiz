using UnityEngine;

public class MonsterRunState : StateMachineBehaviour
{
    public float speed = 2f;
    Transform monster;
    Transform player;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        monster = animator.transform;
        player  = GameObject.FindWithTag("Player").transform;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player == null) return;

        Vector2 dir = (player.position - monster.position).normalized;
        monster.Translate(dir * speed * Time.deltaTime);

        if (Vector2.Distance(monster.position, player.position) < 0.5f)
        {
            animator.SetTrigger("Attack");
        }
    }
}