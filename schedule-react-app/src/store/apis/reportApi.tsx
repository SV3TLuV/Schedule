import {downloadFile} from "../../utils/downloadFile.ts";
import {API_URL} from "../../configuration.ts";
import {ApiTags} from "./apiTags.ts";

export async function downloadReportForDate(dateId: number, accessToken: string) {
    return await downloadFile(
        `${API_URL}/api/${ApiTags.Report}/date`,
        { dateId: dateId },
        accessToken
    )
}

export async function downloadReportForDateRange(startDateId: number, endDateId: number, accessToken: string) {
    return await downloadFile(
        `${API_URL}/api/${ApiTags.Report}/date-range`,
        {
            startDateId: startDateId,
            endDateId: endDateId,
        },
        accessToken
    )
}