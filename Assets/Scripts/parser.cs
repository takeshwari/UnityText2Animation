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


public class Parser {



    public string target;
    public string subj;
    public string action;
    public string second_target; 

    public string Tags(string input)
    {

        // Path to models extracted from `stanford-parser-3.6.0-models.jar`
        var jarRoot = @"";
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

        for (int i = 0; i < tdl.size(); i++)
        {
            TypedDependency node = (TypedDependency)tdl.get(i);

            string relation = node.reln().getShortName();

            if (relation.Contains("nsubj"))
            {
                IndexedWord act = node.gov();
                //node.dep().getword()
                action = act.value();

                UnityEngine.Debug.Log("This is the action " + action); 

                IndexedWord subject = node.dep();
                subj = subject.value();

                UnityEngine.Debug.Log("This is the subject " + subj);

            }

            if (relation.Contains( "dobj"))
            {
                IndexedWord act = node.gov();
                //node.dep().getword()
                action = act.value();
                UnityEngine.Debug.Log("This is the action " + action);

                IndexedWord tar= node.dep();
                target = tar.value();
                UnityEngine.Debug.Log("This is the target " + target);

            }

            if (relation.Contains("nmod"))
            {
     
                IndexedWord tar_two= node.dep();
                second_target = tar_two.value();
                UnityEngine.Debug.Log("This is the target second " + second_target);

            }
        }

        return tdl.ToString();

    }


}
