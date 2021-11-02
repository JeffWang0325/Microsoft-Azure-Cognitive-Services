# Description
This project combines multiple operations in **Microsoft Azure Cognitive Services** into one GUI, including **QnA Maker**, **LUIS**, **Computer Vision**, **Custom Vision**, **Face**, **Form Recognizer**, **Text To Speech**, **Speech To Text** and **Speech Translation**.

It's very user-friendly for users to implement any operation mentioned above. Once the execute button is clicked, the program will connect to the Azure cloud by calling the **REST API**, and then return the response to the GUI to display the result.

It is written in C# and uses Windows Forms for its graphical user interface (GUI).

# Software Environment
| IDE                         | Visual Studio 2019       |
| :-------------------------- | :----------------------- |
| .NET Core                   | .NET Core 3.1            |
| Programming Language        | C#                       |

# GUI Demo:

Please click the following figures or links to watch GUI demo videos:  
[Microsoft Azure Cognitive Services using C#-中文版](https://youtu.be/Z_srnaLAVeg)  
[![Everything Is AWESOME](http://img.youtube.com/vi/Z_srnaLAVeg/sddefault.jpg)](https://youtu.be/Z_srnaLAVeg)    

## ※Outline:   
**1. Overall Structure**
![](https://github.com/JeffWang0325/Microsoft-Azure-Cognitive-Services/blob/master/README%20Image/01.jpg)

**2. Language - QnA Maker in Azure**
![](https://github.com/JeffWang0325/Microsoft-Azure-Cognitive-Services/blob/master/README%20Image/02.jpg)
●Construct the Q&A's knowledge base in Azure **QnA Maker** service, and then train and test the model, and finally publish the API. 

**3. Language - LUIS in Azure**
![](https://github.com/JeffWang0325/Microsoft-Azure-Cognitive-Services/blob/master/README%20Image/03.jpg)
●Construct the LUIS's **Intents**, **Entities** and the corresponding example sentences in Azure **LUIS** service, and then train and test the model, and finally publish the API.

**4. Language - Config Setting**
![](https://github.com/JeffWang0325/Microsoft-Azure-Cognitive-Services/blob/master/README%20Image/04.jpg)

**5. Language - QnA Maker**
![](https://github.com/JeffWang0325/Microsoft-Azure-Cognitive-Services/blob/master/README%20Image/05.jpg)
●Users can easily ask questions and get answers.

**6. Language - LUIS**
![](https://github.com/JeffWang0325/Microsoft-Azure-Cognitive-Services/blob/master/README%20Image/06.jpg)
●Analyze a sentence, and then get the intent, entities and the corresponding entity typies.

**7. Vision - Computer Vision**  
●**Analyze Image**: Analyze the image, and then get some information, including gender, age, summary, categories, tags, adult, color scheme, landmarks, image type, etc.
![](https://github.com/JeffWang0325/Microsoft-Azure-Cognitive-Services/blob/master/README%20Image/07-1.jpg)

●**Detect Object**: Detect all possible objects in the image, and then get the object class and the corresponding confidence.
![](https://github.com/JeffWang0325/Microsoft-Azure-Cognitive-Services/blob/master/README%20Image/07-2.jpg)

●**Read Text (OCR)**: **OCR** means **Optical Character Recognition**. This operation can detect the texts and url links in the image and display them in the right information field.
![](https://github.com/JeffWang0325/Microsoft-Azure-Cognitive-Services/blob/master/README%20Image/07-3.jpg)

**8. Vision - Custom Vision**  
●**Custom Vision** is classified into two categories: **Classification** and **Object Detection**. It allows users to train customized models according to their needs.

●**Classification**:
![](https://github.com/JeffWang0325/Microsoft-Azure-Cognitive-Services/blob/master/README%20Image/08-1.jpg)

●**Object Detection**:
![](https://github.com/JeffWang0325/Microsoft-Azure-Cognitive-Services/blob/master/README%20Image/08-2.jpg)

●**Note**: **Probability Threshold** must be properly determined by a large amount of data verification.

**9. Vision - Face**
![](https://github.com/JeffWang0325/Microsoft-Azure-Cognitive-Services/blob/master/README%20Image/09.jpg)
●Detect all possible faces in the image, and get the information for each face, such as accessories, age, blur, emotion, exposure, facial hair, gender, glasses, hair, head pose, make-up, noise, occlusion, smile, etc.

**10. Vision - Form Recognizer**
![](https://github.com/JeffWang0325/Microsoft-Azure-Cognitive-Services/blob/master/README%20Image/10.jpg)
●Recognize the receipt information in the image.

**11. Speech - Text To Speech**
![](https://github.com/JeffWang0325/Microsoft-Azure-Cognitive-Services/blob/master/README%20Image/11.jpg)
●Convert text to speech based on different languages. The output voice can select gender and voice name.

**12. Speech - Speech To Text**
![](https://github.com/JeffWang0325/Microsoft-Azure-Cognitive-Services/blob/master/README%20Image/12.jpg)
●Convert speech to text based on different languages.  
●Its largest advantage is that it does not need to be assigned a specific language, it will detect which language the voice belongs to by itself.  
●Thus, I think this is a practical tool that allows us to practice pronunciation.

**13. Speech - Speech Translation**
![](https://github.com/JeffWang0325/Microsoft-Azure-Cognitive-Services/blob/master/README%20Image/13.jpg)
●**Speech Translation** can translate a speech into multiple languages. The output voice can select gender and voice name.

---
# Contact Information:
If you have any questions or suggestions about code, project or any other topics, please feel free to contact me and discuss with me. 😄😄😄

<a href="https://www.linkedin.com/in/tzu-wei-wang-a09707157" target="_blank"><img src="https://github.com/JeffWang0325/JeffWang0325/blob/master/Icon%20Image/linkedin_64.png" width="64"></a>
<a href="https://www.youtube.com/channel/UC9nOeQSWp0PQJPtUaZYwQBQ" target="_blank"><img src="https://github.com/JeffWang0325/JeffWang0325/blob/master/Icon%20Image/youtube_64.png" width="64"></a>
<a href="https://www.facebook.com/tzuwei.wang.33/" target="_blank"><img src="https://github.com/JeffWang0325/JeffWang0325/blob/master/Icon%20Image/facebook_64.png" width="64"></a>
<a href="https://www.instagram.com/tzuweiw/" target="_blank"><img src="https://github.com/JeffWang0325/JeffWang0325/blob/master/Icon%20Image/instagram_64.png" width="64"></a>
<a href="https://www.kaggle.com/tzuweiwang" target="_blank"><img src="https://github.com/JeffWang0325/JeffWang0325/blob/master/Icon%20Image/kaggle_64.png" width="64"></a>
<a href="https://github.com/JeffWang0325" target="_blank"><img src="https://github.com/JeffWang0325/JeffWang0325/blob/master/Icon%20Image/github_64.png" width="64"></a>
