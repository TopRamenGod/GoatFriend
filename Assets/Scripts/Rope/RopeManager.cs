using UnityEngine;
using System.Collections;



//[ExecuteInEditMode]
public class RopeManager : MonoBehaviour {

    public static float ROPE_END_OFFSET = 0.231f;

    LineRenderer line;
    public Transform RopeStart;
    public Transform RopeEnd;
    public RopeSegment[] segments;

    public GoatManager Goat;

    public bool Broken{get; private set;}

    //Number of enabled segemnts
    int numActiveSegments;


   // public RopeSegment TestBreakPoint;

    void Start(){
        Broken = false;

        numActiveSegments = segments.Length;

        SetLineRenderer();
       // StartCoroutine(BreakTest());
    }

    //call when new rope is initialized
    public void SetLineRenderer(){
        line = GetComponent<LineRenderer>();
        line.SetVertexCount(numActiveSegments + 2);
    }
	
	// Update is called once per frame
	void Update () {
        _render();
	}


    /// <summary>
    /// Renders the rope.
    /// </summary>
    void _render(){

        //Lines from base to 1st hinged segment
        line.SetPosition(0, RopeStart.position);
  
        //Lines between hinged segments
        for ( int sIndex = 0; sIndex < numActiveSegments ; sIndex++ ){
            line.SetPosition(sIndex+1, segments[sIndex].transform.position);
        }

        //Line to end
        line.SetPosition(numActiveSegments + 1 , RopeEnd.position);
    }

    //Breaks the rope at hinge with specified Segment
    public void BreakAt( RopeSegment segment){

        if( !Broken ){

            //Add rope end to previous segment
            int prev = segment.index - 1;

            //handle case when player slices the first/Last segment
            if ( prev < 0) prev = 0;


            Transform prevT = segments[prev].transform;
            GameObject breakEnd = new GameObject("RopeEnd");
            breakEnd.transform.parent = prevT;
            breakEnd.transform.localPosition = new Vector3(0, - ROPE_END_OFFSET, 0);

            //Resize number of segments
            numActiveSegments = prev + 1;
            this.RopeEnd = breakEnd.transform;

            SetLineRenderer();

            //release the goat
            Goat.ReleaseGoat();

            Broken = true;
        }
   

    }


//    IEnumerator BreakTest(){
//        yield return new WaitForSeconds(3.0f);
//        this.BreakAt(TestBreakPoint);
//    }
//
}
