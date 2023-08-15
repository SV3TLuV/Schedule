import {downloadFile} from "../../utils/downloadFile.ts";
import {ApiTags} from "./apiTags.ts";

export async function downloadTimetableReport(startDateId: number, endDateId: number, accessToken: string) {
    return await downloadFile(
        `${import.meta.env.VITE_API_URL}/api/${ApiTags.Report}/timetable`,
        {
            startDateId: startDateId,
            endDateId: endDateId,
        },
        accessToken
    )
}