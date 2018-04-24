using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using java.io;
using edu.stanford.nlp.process;
using edu.stanford.nlp.ling;
using edu.stanford.nlp.trees;
using edu.stanford.nlp.parser.lexparser;
using Console = System.Console;
using java.lang;

public class TextInput : MonoBehaviour
{

    public GameObject hand;
    InputField input;
    InputField.SubmitEvent se;
    InputField.OnChangeEvent ce;
    public Text output;
    public bool what; 
    private readonly string td1;

    void Start()
    {


        input = this.GetComponent<InputField>();
        se = new InputField.SubmitEvent();
        se.AddListener(SubmitInput);

        ce = new InputField.OnChangeEvent();
        ce.AddListener(ChangeInput);

        input.onEndEdit = se;
        input.onValueChanged = ce;

        // if (hand = 'nsubj')
        //hand = GameObject.FindGameObjectWithTag("hand");
        //hand.SetActive(false); 

    }


    private void SubmitInput(string arg0)
    {

 
        string currentText = output.text;

        string newText = currentText + "\n" + arg0;
        UnityEngine.Debug.Log(newText);
        output.text = newText;

        input.text = "";
        input.ActivateInputField();

        output.text = Tags(newText) ;

       // hand.SetActive(true); 

        //what = true; 

    }


    private void ChangeInput(string arg0)
    {
        UnityEngine.Debug.Log(arg0);
    }

    public string Tags(string input)
    {

        // Path to models extracted from `stanford-parser-3.6.0-models.jar`
        var jarRoot = @"C:\Users\TAKKA\Documents\ResearchProject\Unity-Text-input-output-example\Assets\standford\stanford-parser-full-2018-02-27\models\";
        var modelsDirectory = jarRoot;

        var lp = LexicalizedParser.loadModel(modelsDirectory + @"\lexparser\englishPCFG.ser.gz");


        // This option shows loading and using an explicit tokenizer
        var sent2 = input;
        var tokenizerFactory = PTBTokenizer.factory(new CoreLabelTokenFactory(), "");
        var sent2Reader = new java.io.StringReader(sent2);
        var rawWords2 = tokenizerFactory.getTokenizer(sent2Reader).tokenize();
        sent2Reader.close();
        var tree2 = lp.apply(rawWords2);

        // Extract dependencies from lexical tree
        var tlp = new PennTreebankLanguagePack();
        var gsf = tlp.grammaticalStructureFactory();
        var gs = gsf.newGrammaticalStructure(tree2);
        var tdl = gs.typedDependenciesCCprocessed();
     

        // Extract collapsed dependencies from parsed tree
        var tp = new TreePrint("penn,typedDependenciesCollapsed");
        UnityEngine.Debug.Log(tdl);
        //tp.printTree(tree2);

        return tdl.ToString();  

    }
}
