using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audience : MonoBehaviour
{
    [SerializeField] private float CheerTime;
    
    [Header("References")]
    [SerializeField] private Material[] materials;
    [SerializeField] private AnimationClip[] animClips;
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
    [SerializeField] private Animation anim;

    private bool isCheering;

    void Start()
    {
        transform.LookAt(new Vector3(0, transform.position.y));

        setRandomMaterial();
        TransitionManager.OnTransitionToGameStart += StartCheering;
        TransitionManager.OnTransitionToGame += StopCheering;
        TransitionManager.OnTransitionToTitleStart += StartCheeringWithStop;
        GameManager.OnGameSarted += StartCheeringWithStop;
        Ball.OnFirstThrow += StartCheeringWithStop;

    }

    private void OnDestroy()
    {
        TransitionManager.OnTransitionToGameStart -= StartCheering;
        TransitionManager.OnTransitionToGame -= StopCheering;
        TransitionManager.OnTransitionToTitleStart -= StartCheeringWithStop;
        GameManager.OnGameSarted -= StartCheeringWithStop;
        Ball.OnFirstThrow -= StartCheeringWithStop;
    }

    private void Update()
    {
        if (anim.isPlaying) return;

        if (isCheering)
            playRandomAnim();
        else
            anim.CrossFade("idle");
    }

    private void setRandomMaterial()
    {
        Material rndMat = materials[Random.Range(0, materials.Length)];
        skinnedMeshRenderer.material = rndMat;
    }

    private void playRandomAnim()
    {
        anim.CrossFade(animClips[Random.Range(0, animClips.Length)].name);
    }

    private void StartCheering()
    {
        isCheering = true;
        playRandomAnim();
    }

    private void StopCheering()
    {
        isCheering = false;
        CancelInvoke(nameof(StopCheering));
    }

    private void StartCheeringWithStop()
    {
        isCheering = true;
        playRandomAnim();
        Invoke(nameof(StopCheering), CheerTime);
    }




}
