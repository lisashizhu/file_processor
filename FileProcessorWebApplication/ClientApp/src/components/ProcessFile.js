import React, { useState, useEffect } from 'react';

export function ProcessFile() {
    const [fileContent, setFileContent] = useState('');
    const [isFormValid, setIsFormValid] = useState(false);

    useEffect(() => {
        let isFormValid = fileContent !== '';
        setIsFormValid(isFormValid);
    }, [fileContent]);

    const handleFileSelection = (event) => {
        event.preventDefault();
        let file = event.target.files[0];
        console.log(file);
        if (file.size > 2097152) {
            alert("File cannot be bigger than 2MB!");
            event.target.value = null;
        } else {
            let reader = new FileReader();
            reader.onload = (event) => {
                let text = (event.target.result);
                setFileContent(typeof text === 'string' ? text : ArrayBuffer.from(text).toString());
            }
            reader.readAsText(file);
        }
    }

    async function processFile() {
        let url = 'File/Save';
        let request = {
            "fileContent": fileContent,
            "fileType": "Csv"
        }

        let response = await fetch(url, {
            method: 'Post',
            headers: {
                'Accept': 'application/json',
                'Content-Type':'application/json'
            },
            body: JSON.stringify(request)
        }).then(async function (response) {
            if (response.ok) {
                alert("Done!");
            }
            else {
                let errorRes = await response.json();
                let error = errorRes.error!==''?errorRes.error:'File cannot be saved!';
                alert(error);
            }
        })
    };


    return <div>
        <h2>File Processor</h2>
        <form>
            <div className="form-group">
                <label id="file_processor_selection_label_id">Please select file(*only support .csv file which is less than 2MB.)</label>
                <input id="file_processor_selection_input_id"
                    type="file"
                    className="form-control-file border"
                    accept=".csv"
                    required
                    onChange={handleFileSelection}/>
            </div>
            <div>
                <button type="button" className="btn btn-primary" onClick={processFile} disabled={!isFormValid}>Save</button>
            </div>
        </form>
        </div>
}