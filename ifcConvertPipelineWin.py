import os
import ifcopenshell
import ifcopenshell.validate
import subprocess

print("Enter the ifc file to be converted:")
# input
ifcFileName = input()

print("Give the output filename with format as follows: name.xml or name.dae: ")
# input
outputFileName = input()

flag = os.system("IfcConvert "+ifcFileName+" "+outputFileName+" --use-element-guids")
os.system("IfcConvert "+ifcFileName+" xmlconvert.xml")
if(flag == 0):
	print("Conversion successful")

##Parse properties

model = ifcopenshell.open(ifcFileName)

#Get materials and geometry of a global object

relation = model.by_type("IfcRelAssociatesMaterial") #iterate through relations

for rel in relation:
    objInfo = model.by_guid(rel[0])
    #print(rel.RelatingMaterial[0][0][0][0][0]) Get IfcMaterial
    print(rel.RelatedObjects[0][0]) #Getting global_id
    product = model.by_type(rel.RelatedObjects[0].is_a()) #iterate through beams
    #if product != "IfcWallType":
	thickness = 0
    for item in product:
        if "Type" not in product[0].is_a() and "Style" not in product[0].is_a() :
            if item.Representation.is_a('IfcProductDefinitionShape'):
                geometry = item.Representation.Representations[0].Items[0]
                if(geometry.is_a('IfcExtrudedAreaSolid')):
                    if rel.RelatingMaterial.is_a() == "IfcMaterial":
                        material = rel.RelatingMaterial[0]
                    else:
                        material= rel.RelatingMaterial[0][0][0][0][0]
                        thickness = rel.RelatingMaterial[0][0][0][1]
                    print(" Material: "+str(material))
                    obj.set("material",str(material))
                    obj.set("thickness",str(thickness))
                    loc = xmlv.SubElement(obj, "location")
                    loc.text = str(geometry.Position.Location[0])
                    depth = xmlv.SubElement(obj, "depth")
                    depth.text = str(geometry.Depth)

tree = xmlv.ElementTree(root)

with open ("convertxmlproperties.xml", "wb") as files :
    tree.write(files)
	print("xml properties file created")





print("Do you want to create a unity project and import the ifc assets? Give the input as 'Y' for yes and 'N' for no:")
projectFlag = input()

if(projectFlag == 'Y'):
	print("Please gibe the project path/name")
	projectName = input()
	os.system("C:\Program Files\Unity\Hub\Editor\Unity.exe -createProject "+projectName)
	print("Completed project creation")
	print("Importing dae files to the project.............")
	os.system("copy "+outputFileName +" "+projectName+"\Assets")
	os.system("copy xmlconvert.xml "+projectName+"\Assets")
	print("Import completed!")
