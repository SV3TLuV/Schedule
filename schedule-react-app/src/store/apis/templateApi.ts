import {ApiTags, baseApi} from "./baseApi.ts";
import {IPagedList} from "../../features/models/IPagedList.ts";
import {ITemplate} from "../../features/models/ITemplate.ts";
import {buildUrlArguments} from "../../utils/buildUrlArguments.ts";
import {HttpMethod} from "../../common/enums/HttpMethod.ts";
import {IGetTemplateListQuery} from "../../features/queries/IGetTemplateListQuery.ts";

export const templateApi = baseApi.injectEndpoints({
    endpoints: builder => ({
        getTemplates: builder.query<IPagedList<ITemplate>, IGetTemplateListQuery | void>({
            query: query => ({
                url: `${ApiTags.Template}?${buildUrlArguments(query ?? {})}`,
                method: HttpMethod.GET,
            }),
            providesTags: (result, _, arg) => [
                ...(result?.items ?? []).map(({id}) => ({type: ApiTags.Template, id} as const)),
                {
                    type: ApiTags.Template,
                    id: 'LIST',
                    page: result?.pageNumber,
                    weekTypeId: arg?.weekTypeId,
                    termId: arg?.termId,
                    dayId: arg?.dayId,
                }
            ]
        }),
        getTemplate: builder.query<ITemplate, number>({
            query: id => ({
                url: `${ApiTags.Template}/${id}`,
                method: HttpMethod.GET
            }),
            providesTags: (_, __, id) => [{type: ApiTags.Template, id}]
        }),
    }),
})

export const {
    useGetTemplatesQuery,
    useLazyGetTemplatesQuery,
    useGetTemplateQuery,
    useLazyGetTemplateQuery,
} = templateApi