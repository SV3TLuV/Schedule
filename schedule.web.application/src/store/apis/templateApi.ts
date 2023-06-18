import {baseApi} from "./baseApi.ts";
import {IPagedList} from "../../features/models";
import {ITemplate} from "../../features/models";
import {buildUrlArguments} from "../../utils/buildUrlArguments.ts";
import {HttpMethod} from "../../common/enums";
import {IGetTemplateListQuery} from "../../features/queries";
import {ApiTags} from "./apiTags.ts";

export const templateApi = baseApi.injectEndpoints({
    endpoints: builder => ({
        getTemplates: builder.query<IPagedList<ITemplate>, IGetTemplateListQuery | void>({
            query: query => ({
                url: `${ApiTags.Template}?${buildUrlArguments(query ?? {})}`,
                method: HttpMethod.GET,
            }),
            providesTags: result => [
                ...(result?.items ?? []).map(({id}) => ({type: ApiTags.Template, id} as const)),
                {
                    type: ApiTags.Template,
                    id: 'LIST',
                    page: result?.pageNumber,
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