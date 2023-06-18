import axios from "axios";
import {HttpMethod} from "../common/enums";
import {buildUrlArguments} from "./buildUrlArguments.ts";

export async function downloadFile(url: string, params: object, accessToken: string) {
    try {
        const response = await axios({
            method: HttpMethod.GET,
            url: `${url}?${buildUrlArguments(params)}`,
            headers: {
                'Authorization': `Bearer ${accessToken}`
            },
            withCredentials: true,
            responseType: 'blob'
        })

        const contentDisposition = response.headers['content-disposition'];
        const fileName = contentDisposition?.split('filename=')[1];
        const blob = new Blob([response.data])
        const downloadUrl = window.URL.createObjectURL(blob)
        const link = document.createElement('a')
        link.href = downloadUrl
        link.setAttribute('download', fileName)
        document.body.appendChild(link)
        link.click()
        link.remove()
        window.URL.revokeObjectURL(downloadUrl)
    } catch (error) {
        console.error(error);
    }
}