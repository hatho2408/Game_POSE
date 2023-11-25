using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FruitType
{
    apple,
    banana,
    cherry,
    kiwi,
    melon,
    orange,
    pineapple,
    strawberry
}
public class Fruit_Item_Pixel : MonoBehaviour
{
    [SerializeField] protected Animator anim;
    [SerializeField] protected SpriteRenderer sr;
    public FruitType myFruitType;
    [SerializeField] private Sprite[] fruitImage;

    [SerializeField] private GameObject pickUpFx;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player_Pixel>()!= null)
        {
            PlayerManager_Pixel.instance.fruits++;
            AudioManager_Pixel.instance.PlaySFX(7);

            if (pickUpFx != null)
            {
                GameObject newPickUpFx = Instantiate(pickUpFx, transform.position, transform.rotation);
                Destroy(newPickUpFx, .5f);
            }

            Destroy(gameObject);
        }
    }


    public void FruitSteup(int fruitIndex)
    {
        for (int i = 0; i < anim.layerCount; i++)
        {
            anim.SetLayerWeight(i, 0);
        }

        anim.SetLayerWeight(fruitIndex, 1);
    }

    //private void OnValidate()
    //{
    //    sr.sprite = fruitImage[((int)myFruitType)];
    //}
    //ctrl + k + c  \ ctrl + k + u
}
