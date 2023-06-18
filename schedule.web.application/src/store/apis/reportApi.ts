import {downloadFile} from "../../utils/downloadFile.ts";
import {API_URL} from "../../configuration.ts";
import {ApiTags} from "./apiTags.ts";

export async function downloadTimetableReport(startDateId: number, endDateId: number, accessToken: string) {
    return await downloadFile(
        `${API_URL}/api/${ApiTags.Report}/timetable`,
        {
            startDateId: startDateId,
            endDateId: endDateId,
        },
        accessToken
    )
}