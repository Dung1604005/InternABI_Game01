using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

/// <summary>
/// A utility class, PredefinedAssemblyUtil, provides methods to interact with predefined assemblies.
/// It allows to get all types in the current AppDomain that implement from a specific Interface type.
/// </summary>
public static class PredefinedAssemblyUtil
{
    //Unity divide our code into 4 file dll which is defined below for navigation
    enum AssemblyType
    {
        AssemblyCSharp, // code game
        AssemblyCSharpEditor, // scrip custom editor

        AssemblyCSharpEditorFirstPass, // code editor of plugin

        AssemblyCSharpFirstPass // plugin from assetstore
    }
    /// <summary>
    /// maping the assemblyname corresponding to AssemblyType
    /// </summary>
    /// <param name="assemblyName"></param>
    /// <returns></returns>

    static AssemblyType? GetAssemblyByType(string assemblyName)
    {
        return assemblyName switch
        {
            "Assembly-CSharp" => AssemblyType.AssemblyCSharp,
            "Assembly-CSharp-Editor" => AssemblyType.AssemblyCSharpEditor,
            "Assembly-CSharp-Editor-firstpass" => AssemblyType.AssemblyCSharpEditorFirstPass,
             "Assembly-CSharp-firstpass" => AssemblyType.AssemblyCSharpFirstPass,
             _ => null

        };
    }
    /// <summary>
    /// Method looks through a given assembly and adds types that fulfill a certain interface to the provided collection.
    /// </summary>
    /// <param name="assemblyTypes"></param>
    /// <param name="interfaceType"></param>
    /// <param name="results"></param>

    static void AddTypesFromAssembly(Type[] assemblyTypes, Type interfaceType, ICollection<Type> results)
    {
        if (assemblyTypes == null) return;
        // Loop for every class, struct in assembly file
        for(int i = 0; i < assemblyTypes.Length; i++)
        {
            Type type= assemblyTypes[i];
            // Check if interfaceType is can be assign from type

            if(type != interfaceType && interfaceType.IsAssignableFrom(type))
            {
                results.Add(type);
            }
        }
    }

    /// <summary>
    /// Gets all Types from all assemblies in the current AppDomain that implement the provided interface type.
    /// </summary>
    /// <param name="interfaceType"></param>
    /// <returns></returns>

    public static List<Type> GetTypes(Type interfaceType)
    {
        
        Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

        Dictionary<AssemblyType, Type[]> assemblyTypes = new Dictionary<AssemblyType, Type[]>();

        List<Type> listTypes = new List<Type>();

        for(int i =0; i < assemblies.Length; i++)
        {
            AssemblyType? assemblyType = GetAssemblyByType(assemblies[i].GetName().Name);
            if(assemblyType != null)
            {
                assemblyTypes.Add((AssemblyType) assemblyType, assemblies[i].GetTypes());
            }
        }

        assemblyTypes.TryGetValue(AssemblyType.AssemblyCSharp , out var assemblyCSharpTypes);
        AddTypesFromAssembly(assemblyCSharpTypes, interfaceType, listTypes);

        assemblyTypes.TryGetValue(AssemblyType.AssemblyCSharpFirstPass , out var assemblyCSharpFirstPassTypes);
        AddTypesFromAssembly(assemblyCSharpFirstPassTypes, interfaceType, listTypes);

        return listTypes;
    }




}
