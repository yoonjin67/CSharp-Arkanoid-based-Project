using UnityEngine;
using System.Collections.Generic;

public class CreateBlock_DevGirl_LineTwo : MonoBehaviour
{
    [SerializeField] GameObject block;
    [SerializeField] Vector2 pos = new Vector2(-1.6f, 3.7f);
    [SerializeField] Vector2 offset = new Vector2(0.32f, -0.14f);
    int col = 10;
    int row = 3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void CreateLineOne() {
        for(int i = 0; i < row; i++) {
            for(int j = 0; j < col; j++) {
                Instantiate(block, new Vector2(pos.x + (j*offset.x), pos.y + (i*offset.y)),Quaternion.identity);
            }
        }
    }
    void Start()
    {
       CreateLineOne(); 
    }
}
