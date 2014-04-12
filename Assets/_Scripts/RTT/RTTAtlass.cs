using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class RTTAtlass : RTTDisplay
{
    public bool removeAfterInit = true;
    public bool useSpriteUV = true;
    public Vector2 spriteRegions = new Vector2(2,2);
    public RTTSprite sprite;
    [Range(0, 1000)]
    public int spriteCount = 4;
    public List<RTTSprite> sprites;

    private Vector2 uvScaler = new Vector2();
    private Vector2 uvTranslator = new Vector2();

    public void Start()
    {
        base.Start();

        uvScaler.x = 1 / (float)spriteRegions.x;
        uvScaler.y = 1 / (float)spriteRegions.y;

        uvTranslator.x = uvScaler.x;
        uvTranslator.y = uvScaler.y;

        Material mat;
        Vector2 region;

        int count = sprites.Count;
        if (count == 0 && sprite != null && spriteCount > 0)
        {
            count = spriteCount;
            GameObject spriteContainer = new GameObject("Sprite Container");
            while (count-- > 0)
            {
                RTTSprite sprt = Instantiate(sprite) as RTTSprite;
                sprt.transform.position = UnityEngine.Random.insideUnitCircle*8;
                sprt.transform.parent = spriteContainer.transform;
                region = sprt.region;
                if (region.x < 0) region.x = UnityEngine.Random.Range(0, (int) spriteRegions.x);
                if (region.y < 0) region.y = UnityEngine.Random.Range(0, (int) spriteRegions.y);
               
                if (useSpriteUV)
                {
                    if (region.x > 2) region.x -= 1;
                    if (region.y > 2) region.y -= 1;
                    region = new Vector2(uvTranslator.x * region.x, uvTranslator.y * region.y);
                    sprt.renderer.sharedMaterial = this.renderer.material;
                    sprt.SetupUVS(region, region + uvTranslator);
                    sprt.transform.Rotate(new Vector3(0, 0, -90));
                }
                else
                {
                    region = new Vector2(uvTranslator.x * region.x, uvTranslator.y * region.y);
                    mat = sprt.renderer.material;
                    SetupMaterialUVOffset(ref mat, region, uvTranslator);
                    Transform sprtTransform = sprt.transform;
                    ReverseSpriteY(ref sprtTransform);
                }

                sprites.Add(sprt);
                sprt.gameObject.isStatic = true;
            }
        }
        else
        {
            while (count-- > 0)
            {
                RTTSprite sprt = sprites[count] as RTTSprite;
                region = sprt.region;
                region = new Vector2(uvTranslator.x * region.x, uvTranslator.y * region.y);
                if (useSpriteUV)
                {
                    sprt.renderer.sharedMaterial = this.renderer.material;
                    sprt.SetupUVS(region, region + uvTranslator);
                    sprt.transform.Rotate(new Vector3(0, 0, -90));
                }
                else
                {
                    mat = sprt.renderer.material;
                    mat.mainTexture = this.renderer.material.mainTexture;
                    SetupMaterialUVOffset(ref mat, region, uvTranslator);
                    Transform sprtTransform = sprt.transform;
                    ReverseSpriteY(ref sprtTransform); 
                }
            }
        }
    }

    void Update()
    {
        base.Update();
        if (this.isinited)
            if (removeAfterInit) Destroy(this.gameObject);
            else this.gameObject.active = false;
    }

    private void SetupMaterialUVOffset(ref Material mat, Vector2 region, Vector2 scale)
    {
        mat.mainTexture = this.renderer.material.mainTexture;
        mat.SetTextureScale("_MainTex", uvTranslator);
        mat.SetTextureOffset("_MainTex", region);
    }

    private void ReverseSpriteY(ref Transform sprtTransform)
    {
        Vector3 scale = sprtTransform.localScale;
        scale.y *= -1;
        sprtTransform.localScale = scale;
    }
}
