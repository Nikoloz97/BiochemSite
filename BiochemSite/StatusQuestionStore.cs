﻿using BiochemSite.Models;

namespace BiochemSite
{
    public class StatusQuestionStore
    {
        public List<StatusQuestionDto> StatusQuestions { get; set; }

        public static StatusQuestionStore Instance { get; }  = new StatusQuestionStore();

        public StatusQuestionStore()
        {
            StatusQuestions = new List<StatusQuestionDto>()
            {
                new StatusQuestionDto() {Id = 1, ChapterNum = 1, SubchapterNum = 1, Prompt = "What is this amino acid?", Explanation = "Lysine is a base that contains and R-group that consists of an amine followed by four carbons", ImageURL = "https://www.sigmaaldrich.com/deepweb/content/dam/sigma-aldrich/structure6/007/mfcd00064433.eps/_jcr_content/renditions/mfcd00064433-large.png"},
                new StatusQuestionDto() {Id = 2, ChapterNum = 1, SubchapterNum = 2, Prompt = "What is a pka", Explanation = "It is the tendency for a functional group to dissociate", ImageURL = "https://pubchem.ncbi.nlm.nih.gov/image/imgsrv.fcgi?cid=5962&t=l"},
                new StatusQuestionDto() {Id = 3, ChapterNum = 2, SubchapterNum = 1, Prompt = "What is a base", Explanation = "Something that wants to be protonated", ImageURL = "https://i5.walmartimages.com/asr/88ae3b61-6328-43df-8f19-015b618e8bde.90e6fa1a34da90da690050d0a2bd8d5f.jpeg"},
            };
        }


    }
}
