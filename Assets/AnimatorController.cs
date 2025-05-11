using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] 
    private Animator _animator;
    protected void Start()
    {
        _animator.SetFloat("Speed", 1f);
    }

    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _animator.SetTrigger("Jump");
        }
    }
}
